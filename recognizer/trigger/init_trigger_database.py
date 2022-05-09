from trigger.trigger_database import TriggerDatabase

if __name__ == "__main__":
    db = TriggerDatabase()
    db.clear_data()
    db.add_trigger("flame", ["flame", "lame", "fame"], "horizontal_line")
    # db.add_trigger("shield", ["shield"], "circle")
    db.add_trigger("cure", ["cure", "pure", "your", "you're"], "ex")
    db.add_trigger("lightning", ["lightning"], "vertical_line")
    db.add_trigger("leech", ["leech"], "wave")
    # db.add_trigger("hide", ["hide"], "less_than")
    db.save_data()
