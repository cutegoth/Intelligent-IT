from Elements.fact import Fact


class Rule:

    def __init__(self, name_rule, question, update_fact, priority, value):
        self.name = name_rule
        self.question = question
        self.update_fact = update_fact
        self.is_used = False
        self.priority = priority
        self.value = value
        self.own_facts = []
        self.count_condition = 0

    def add_condition(self, name, value):
        # fact = Fact(name, value)
        str = f'{name}, {value}'
        self.own_facts.append(str)




