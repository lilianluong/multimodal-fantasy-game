from typing import Dict, List, Tuple

import matplotlib.pyplot as plt
import numpy as np

import config
from gesture.gesture_types import Gesture, GestureDatabase
from gesture.gesture_util import normalize_frames
from gesture.template_matching import compare_frames


def recognize_gesture(frames: List[Tuple],
                      db: GestureDatabase,
                      method: str = config.GESTURE_RECOGNIZER_METHOD) -> Dict[str, float]:
    """
    Determine if the frames matches the gestures labeled in db.
    :param frames: List of (t, x, y, z)
    :param db: GestureDatabase to compare against
    :param method: string method to use
    :return: dictionary mapping label to similarity metric
    """
    if method == "brute force":
        return recognize_gesture_brute_force(frames, db)


def recognize_gesture_brute_force(frames: List[Tuple], db: GestureDatabase) -> Dict[str, float]:
    """
    Brute force through all windows in the input frames and compare against fixed db templates
    """
    scores = {}
    frames_arr = np.array(frames)
    for label, template in db.iter_templates():
        template_arr = template.to_numpy()
        template_time = template.time
        start = end = 0
        best_score = float('inf')
        best_window = None
        # plot_frames(template_arr, np.array(normalize_frames(frames)))
        while start < len(frames):
            while end < len(frames) and frames_arr[end][0] - frames_arr[start][0] < template_time:
                end += 1
            normalized_window = np.array(normalize_frames(frames[start:end+1]))
            score = compare_frames(normalized_window[:, 1:3], template_arr[:, 1:3])
            # print(start, end, score)
            if score < best_score:
                best_score = score
                best_window = (start, end)
            start += 1
            if end >= len(frames):
                break
        if label not in scores or best_score < scores[label]:
            scores[label] = best_score
            plot_frames(template_arr, np.array(normalize_frames(frames[best_window[0]:best_window[1]+1])))
    return scores


def plot_frames(a: np.ndarray, b: np.ndarray):
    plt.figure()
    first_axis, second_axis = 1, 2
    plt.scatter(a[:, first_axis], a[:, second_axis], color="b")
    plt.scatter(b[:, first_axis], b[:, second_axis], color="r")
    plt.show()
