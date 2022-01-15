from explanation_component import ExplanationComponent
from knowledge_base import KnowledgeBase
from working_memory import WorkingMemory


class InferenceEngine:

    def __init__(self):
        self.knowledge_base = KnowledgeBase()
        self.working_memory = WorkingMemory()
        self.explanation_component = ExplanationComponent()
        self.knowledge_base.load()
        self.current_rule = None
        self.answers = None
        self.question = None
        self.get_answer_question()
        self.isAnswer = False

    def get_answer_question(self):

        current_rules = self.get_current_rules()
        current_rules.sort(key=lambda c: c.priority, reverse=True)  # сортировка правил по приоритету
        current_rules.sort(key=lambda c: c.count_condition) # сортировка правил по количеству условий

        if len(current_rules) == 0:
            self.isAnswer = True
            self.question = self.working_memory.current_facts[-1].value
            return

        self.current_rule = current_rules[0]
        self.knowledge_base.rules[self.knowledge_base.rules.index(self.current_rule)].isUsed = True

        if len(self.current_rule.value) != 0:
            self.question = self.current_rule.question
            self.answers = self.current_rule.value
        else:
            if self.current_rule.update_fact.get('name') in self.working_memory.current_facts_names:
                self.isAnswer = True
                self.question = self.working_memory.current_facts[-1].value
                return

            self.working_memory.add_fact(self.current_rule.update_fact.get('name'),
                                         self.current_rule.update_fact.get('value'))
            self.get_answer_question()

    # def print_rule(self, rules):
    #     arr = []
    #     for i in range(len(rules)):
    #         arr.append(rules[i].name)
    #     return arr

    def get_current_rules(self):
        """
        Метод для получения подходящих правил
        :return: массив с правилами
        """
        current_rules = []

        if len(self.working_memory.current_facts) == 0:
            current_rules = self.knowledge_base.rules
            return current_rules

        for rule in self.knowledge_base.rules:

            if not rule.is_used:

                flag = []
                for condition in rule.own_facts:
                    arr = True
                    for fact in condition:
                        for cur_fact in self.working_memory.current_facts:
                            if fact.name == cur_fact.name:
                                if fact.value != cur_fact.value:
                                    arr = False
                                    break
                    if arr:
                        flag.append(1)
                    else:
                        flag.append(0)

                if sum(flag) > 0:
                    rule.count_condition = sum(flag)
                    current_rules.append(rule)

        return current_rules

    def set_user_answer(self, value):
        self.working_memory.add_fact(self.current_rule.update_fact.get('name'), value)

    def get_logs(self):
        return self.explanation_component.get_logs(self.working_memory.current_facts)