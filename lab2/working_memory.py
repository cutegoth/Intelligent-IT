from Elements.fact import Fact

class WorkingMemory:
    def __init__(self):
        self.current_facts = []
        self.current_facts_names = []

    def add_fact(self, name, value):
        fact = Fact(name, value)
        self.current_facts.append(fact)
        self.current_facts_names.append(name)