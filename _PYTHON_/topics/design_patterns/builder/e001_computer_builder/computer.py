class Computer:

    def display(self):
        print("Custom Computer:")
        print("\t{:>10}: {}".format("Case", self.case))
        print("\t{:>10}: {}".format("Case", self.mainboard))
        print("\t{:>10}: {}".format("Case", self.cpu))
        print("\t{:>10}: {}".format("Case", self.memory))
        print("\t{:>10}: {}".format("Case", self.hard_drive))
        print("\t{:>10}: {}".format("Case", self.video_card))
