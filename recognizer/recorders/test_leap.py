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
        print("Frame id: %d, timestamp: %d, hands: %d, fingers: %d, tools: %d, gestures: %d" % (
          frame.id, frame.timestamp, len(frame.hands), len(frame.fingers), len(frame.tools), len(frame.gestures())))
        last_id = frame.id

print("disconnected from controller")
