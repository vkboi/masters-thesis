import numpy as np
import cv2
import argparse as ap
import os
from skimage.segmentation import watershed
from skimage.feature import canny, peak_local_max
from scipy import ndimage
import imutils


def th_otsu(image: np.ndarray)-> np.ndarray:
    return cv2.threshold(image, 0, 255, cv2.THRESH_BINARY_INV+cv2.THRESH_OTSU)[1]


def fill_holes(grayscale_image: np.ndarray) -> np.ndarray:
    img = th_otsu(grayscale_image)
    img = cv2.morphologyEx(img, cv2.MORPH_OPEN, np.ones((5, 5), np.uint8))
    floodfill_img = cv2.copyMakeBorder(img, 1, 1, 1, 1, cv2.BORDER_CONSTANT, value=0)
    h, w = floodfill_img.shape[:2]
    mask = np.zeros((h + 2, w + 2), np.uint8)
    cv2.floodFill(floodfill_img, mask, (0, 0), 255);
    floodfill_img_inv = cv2.bitwise_not(floodfill_img)
    return img | floodfill_img_inv[1:-1, 1:-1].copy()


def get_segmentation_predictions(image: np.ndarray):
    result = []
    filled = fill_holes(image)
    contours = cv2.findContours(filled, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
    d = ndimage.distance_transform_edt(filled)
    peaks = peak_local_max(d, indices=False, min_distance=40, labels=filled)
    markers = ndimage.label(peaks, structure=np.ones((3, 3)))[0]
    labels = watershed(-d, markers, mask=filled)
    for label in [x for x in np.unique(labels) if x != 0]:
        mask = np.zeros(filled.shape, dtype="uint8")
        mask[labels == label] = 255
        cnts = cv2.findContours(mask.copy(), cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)
        cnts = imutils.grab_contours(cnts)
        c = max(cnts, key=cv2.contourArea)
        x, y, w, h = cv2.boundingRect(c)
        if w * h < 3500 or w * h > 20000:
            continue
        result.append([y, x, y + h, x + w])
    return result


if __name__ == "__main__":
	parser = ap.ArgumentParser()
	parser.add_argument("-i", "--image-path", type=str, help="Szegmentálandó kép rel/abspath.", required=True)
	parser.add_argument("-o", "--output-path", type=str, help="Bounding box output útvonal. Default: 'annotations.txt'.", default="annotations.txt")
	args = vars(parser.parse_args())
	with open(args["output_path"], mode="w") as annotation_file:
		original = cv2.imread(args["image_path"], cv2.IMREAD_COLOR)
		img = cv2.cvtColor(original, cv2.COLOR_BGR2GRAY)
		for c in get_segmentation_predictions(img.copy()):
			annotation_file.write(f"-1 {max(c[1], 0)} {max(c[0], 0)} {min(c[3], img.shape[1])} {min(c[2], img.shape[0])}\n")
