import argparse as ap
import numpy as np
import cv2
import os
import tensorflow as tf
from tensorflow.keras.models import model_from_json
from tensorflow.keras.losses import CategoricalCrossentropy, BinaryCrossentropy
from tensorflow.keras.metrics import Precision, Recall
from tensorflow_addons.optimizers import AdamW
from tensorflow.keras.optimizers import Adam

default_annotations_path = "annotations.txt"
is_relaxed = False


def load_model(model_path: str, weights_path: str):
    with open(model_path, 'r') as model_json:
        loaded_model = model_from_json(model_json.read())

    loaded_model.load_weights(weights_path)
    if is_relaxed:
        loaded_model.compile(optimizer=Adam(learning_rate=0.001), loss=BinaryCrossentropy(), metrics=['accuracy'])
    else:
        loaded_model.compile(optimizer=AdamW(weight_decay=0.001), loss=CategoricalCrossentropy(), metrics=[Precision(), Recall()])
    return loaded_model


def resize_image(img: np.ndarray):
    train_shape = (112, 112, 3)
    img_h, img_w, _ = img.shape
    if img_h == train_shape[0] and img_w == train_shape[1]:
        return img
    scale_h = img_h / train_shape[0]
    scale_w = img_w / train_shape[1]
    if img_h == img_w:
        resized = cv2.resize(img.copy(), (train_shape[1], train_shape[0]))
    elif scale_h > scale_w:
        resized = cv2.resize(img.copy(), (int(train_shape[0] * img_w / img_h), train_shape[0]))
    else:
        resized = cv2.resize(img.copy(), (train_shape[1], int(train_shape[1] * img_h / img_w)))
    return cv2.copyMakeBorder(resized, 0, train_shape[0] - resized.shape[0], 0, train_shape[1] - resized.shape[1], 
                              cv2.BORDER_CONSTANT, value=[0, 0, 0])
	

if __name__ == "__main__":
	parser = ap.ArgumentParser()
	parser.add_argument("-m", "--model-json", type=str, help="Használandó modelt leíró JSON. Default: 'model.json' az src mappában.", default=os.path.join("..", "..", "model.json"))
	parser.add_argument("-w", "--weights", type=str, help="Model súlyait tartalmazó h5 fájl. Default: 'model.h5' az src mappában.", default=os.path.join("..", "..", "model.h5"))
	parser.add_argument("-b", "--binary", "--binary-classification", help="Bináris osztályzás kell. A felismerés azt mondja meg, adott objektum vörösvértest-e.", nargs="?")
	parser.add_argument("-i", "--image-path", type=str, help="Az input kép elérési útvonala. Kötelező.", required=True)
	parser.add_argument("-a", "--annotations", type=str, help=f"Fájl, amelyben az annotációk találhatók. Default: '{default_annotations_path}'. Ha nincs megadva, akkor az {default_annotations_path}-ben az első ; előtt szerepeljen a képfájl elérési útvonala.", default=default_annotations_path)
	parser.add_argument("-o", "--output-path", type=str, help="Célfájl, ahova a predikciók írandók. Default: 'preds.txt'.", default="preds.txt")
	args = vars(parser.parse_args())
	is_relaxed = "binary" in args
	model = load_model(args["model_json"], args["weights"])
	with open(args["annotations"], mode="r") as annotations_file, open(args["output_path"], mode="w") as preds_file:
		bboxes = []
		img_path = args["image_path"]
		for line in annotations_file.readlines():
			if args["annotations"] == "annotations.txt":
				bboxes.append([int(x) for x in line.strip().split(sep=" ")])
			else:
				bboxes.append([int(x) for x in line.strip().split(sep=";")])
		image = cv2.cvtColor(cv2.imread(img_path, cv2.IMREAD_COLOR), cv2.COLOR_BGR2RGB)
		preds = []
		for bbox in bboxes:
			try:
				x = resize_image(image[bbox[0]:bbox[0] + bbox[1], bbox[2]:bbox[2] + bbox[3]])
				pred = model.predict(x[np.newaxis, :])
				preds.append([int(pred), 1] if is_relaxed else [np.argmax(pred), np.max(pred)])
			except cv2.error:
				continue
		preds_file.write(";".join([f"{x[0]} {x[1]:.2f}" for x in preds]) + "\n")
