from recorders.leap import Leap

controller = Leap.Controller()
print(controller.has_focus)
while not controller.is_connected:
    pass
print("Controller connected!")
controller.set_policy(Leap.Controller.POLICY_BACKGROUND_FRAMES)

last_id = -1
while controller.is_connected:
    frame = controller.frame()
    if frame.id != last_id:
        if not frame.hands.is_empty:
            hand_position = frame.hands.rightmost.palm_position
            print(f"{(frame.timestamp,) + hand_position.to_tuple()},")
        last_id = frame.id

print("disconnected from controller")
