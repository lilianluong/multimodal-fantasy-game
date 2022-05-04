from typing import List, Tuple


def normalize_frames(frames: List[Tuple[int, float, float, float]]) -> List[Tuple[int, float, float, float]]:
    min_time = frames[0][0]
    xs = [x for t, x, y, z in frames]
    min_x, max_x = min(xs), max(xs)
    ys = [y for t, x, y, z in frames]
    min_y, max_y = min(ys), max(ys)
    zs = [z for t, x, y, z in frames]
    min_z, max_z = min(zs), max(zs)
    # scale = max(max_x, max_y) - min(min_x, min_y)
    scale = max(max_x - min_x, max_y - min_y)
    # scale = max(max_x, max_y, max_z) - min(min_x, min_y, min_z)
    return [
        (
            t - min_time,
            (x - min_x) / scale,
            (y - min_y) / scale,
            (z - min_z) / scale
        )
        for t, x, y, z in frames
    ]
