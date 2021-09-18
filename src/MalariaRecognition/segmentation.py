import numpy as np
import cv2
import argparse as ap
import os


def th_otsu(image: np.ndarray)-> np.ndarray:
    return cv2.threshold(image, 0, 255, cv2.THRESH_BINARY_INV+cv2.THRESH_OTSU)[1]


def th_adaptive_mean(image: np.ndarray)-> np.ndarray:
    return cv2.adaptiveThreshold(image, 255, cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY, 11, 2)


def th_adaptive_gaussian(image: np.ndarray) -> np.ndarray:
    return cv2.adaptiveThreshold(image, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 11, 2)


def gamma_correction(image: np.ndarray, gamma: float) -> np.ndarray:
    lut = np.empty((1, 256), np.uint8)
    for i in range(256):
        lut[0, i] = np.clip(pow(i / 255.0, gamma) * 255.0, 0, 255)
    return cv2.LUT(image, lut)


def bbox_on_circle(x: int, y: int, radius: int, img_shape: list):
    return [max(x - radius, 0), max(y - radius, 0), min(x + radius, img_shape[1]), min(y + radius, img_shape[0])]


def bounding_box_size(cnt):
    return cv2.boundingRect(cnt)[2] * cv2.boundingRect(cnt)[3]


def contour_to_bbox(contour):
    x, y, w, h = cv2.boundingRect(contour)
    return [x + w, y + h, w, h]


def circle_confidence(bc_center: list, bc_radius: int, threshold_image: np.ndarray) -> float:
    print(bc_center)
    print(bc_radius)
    print(threshold_image.shape)
    #print(bc_center[1] - bc_radius, bc_center[1] + bc_radius)
    #print(bc_center[0] - bc_radius, bc_center[0] + bc_radius)
    bc_img = threshold_image[bc_center[1] - bc_radius:bc_center[1] + bc_radius, bc_center[0] - bc_radius:bc_center[0] + bc_radius]
    bc = np.zeros(bc_img.shape, dtype=np.uint8)
    bc = cv2.circle(bc, (bc_radius, bc_radius), bc_radius, 1, -1)
    print(bc_img)
    print(bc)
    return round(np.sum(cv2.bitwise_and(bc, bc_img)) / (bc_img.shape[0] * bc_img.shape[1]), 4)


def filter_circles(circles: list, th_img: np.ndarray) -> list:
    # confidences = [circle_confidence([x[0], x[1]], x[2], th_img) for x in circles[0]]
    # print(confidences)
    # print(circles)
    return [[x[1], x[0], x[2], x[2]] for i, x in enumerate(circles[0])] #if confidences[i] >= 0.8 or (th_img[x[1], x[0]] > 0 and confidences[i] >= 0.5)]


def detect_local(image: np.ndarray, offset: list) -> list:
    threshold_image = th_otsu(image)
    image = cv2.medianBlur(image, 5)
    circles = cv2.HoughCircles(image, cv2.HOUGH_GRADIENT, 2.5, 60, param1=50, param2=30, minRadius=55, maxRadius=80)
    if circles is None:
        return []
    circles = np.uint16(np.around(circles))
    return [[x[0] + offset[1], x[1] + offset[0], x[2], x[3]] for x in filter_circles(circles, threshold_image) if threshold_image[x[0], x[1]] > 0]

def detect_cells(image: np.ndarray) -> list:
    denoised = cv2.fastNlMeansDenoising(image, h=15)
    clahe_img = clahe.apply(denoised)
    clahe_img = ~th_otsu(clahe_img)
    contours, _ = cv2.findContours(denoised, cv2.RETR_CCOMP, cv2.CHAIN_APPROX_SIMPLE)
    cell_contours = [contour_to_bbox(x) for x in contours if 3500 <= bounding_box_size(x) <= 10000]
    big_contours = [contour_to_bbox(x) for x in contours if bounding_box_size(x) > 10000]
    for c in big_contours:
        cell_contours.extend(detect_local(image[c[1] - c[3]:c[1] + c[3], c[0] - c[2]:c[0] + c[2]], [c[0] - c[2], c[1] - c[3]]))
    return cell_contours
	

if __name__ == "__main__":
	parser = ap.ArgumentParser()
	parser.add_argument("-i", "--image-path", type=str, help="Szegmentálandó kép rel/abspath.", required=True)
	parser.add_argument("-o", "--output-path", type=str, help="Bounding box output útvonal. Default: 'annotations.txt'.", default="annotations.txt")
	args = vars(parser.parse_args())
	img = cv2.imread(args["image_path"], cv2.IMREAD_GRAYSCALE).astype(np.uint8)
	clahe = cv2.createCLAHE()
	contours = detect_cells(img)
	# contour_image = img.copy()
	with open(args["output_path"], mode="w") as annotation_file:
		for c in contours:
			annotation_file.write(f"-1 {max(c[1] - c[3], 0)} {max(c[0] - c[2], 0)} {min(c[1] + c[3], img.shape[1])} {min(c[0] + c[2], img.shape[0])}\n")
			# contour_image = cv2.rectangle(contour_image, (c[1] - c[3], c[0] - c[2]), (c[1] + c[3], c[0] + c[2]), 255, 2)