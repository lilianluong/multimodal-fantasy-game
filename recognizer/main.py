from trigger.trigger_recognizer import TriggerRecognizer
import sys

if __name__ == "__main__":
    argumentList = sys.argv[1:]
    use_gesture = False if "--no_gesture" in argumentList else True
    use_speech = False if "--no_speech" in argumentList else True
    
    trigger_recognizer = TriggerRecognizer(use_gesture, use_speech)
    i = 1
    
    while True:
        print("It's your turn! Turn", i)
        result, score = trigger_recognizer.take_turn(use_gesture, use_speech)
        if result is None:
            print("No spell detected")
        else:
            print("You cast:", result, "- score:", score)
        i += 1
