from trigger.trigger_recognizer import TriggerRecognizer

if __name__ == "__main__":
    trigger_recognizer = TriggerRecognizer()
    i = 1
    while True:
        print("It's your turn! Turn", i)
        result, score = trigger_recognizer.take_turn()
        if result is None:
            print("No spell detected")
        else:
            print("You cast:", result, "- score:", score)
        i += 1
