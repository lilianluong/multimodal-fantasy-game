import numpy as np

import config


def compare_frames(a: np.ndarray, b: np.ndarray, method: str = config.TEMPLATE_MATCHING_METHOD) -> float:
    """
    Returns the similarity score between 3D point clouds
    :param a: (N_a, 3) array
    :param b: (N_b, 3) array
    :param method: str for which method to use
    :return: score
    """
    if method == "modified hausdorff":
        return max(modified_hausdorff_distance(a, b), modified_hausdorff_distance(b, a))


def modified_hausdorff_distance(a: np.ndarray, b: np.ndarray) -> float:
    """
    returns 1/N_a sum_a min_b (||a - b||)
    """
    difference = a[:, None, :] - b[None, :, :]
    distance = np.sqrt(np.sum(difference ** 2, axis=2))
    result = np.mean(np.min(distance, axis=1))
    return result
