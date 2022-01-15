import copy
import json
from Elements.fact import Fact
from Elements.rule import Rule



class KnowledgeBase:
    def __init__(self):
        self.rules = []

    def load(self):
        with open('rule.json', 'r') as file:
            data = json.load(file)

        for item in data:
            rule = Rule(item.get('rulename'), item.get('question'),
                        item.get('updateFact'), item.get('priority'), item.get('value'))

            dixt = item.get('condition')
            operation = dixt.get('operation')
            facts = dixt.get('facts')

            if facts[0].get('facts') != None: # случай вложенности фактов
                arr = []
                if operation == '&':          # случай &
                    for fact in facts:
                        nest = fact.get('operation')
                        for lfact in fact.get('facts'):
                            if nest == '&':
                                arr.append(Fact(lfact.get('name'), lfact.get('value')))
                            else:
                                newarr = copy.copy(arr)
                                newarr.append(Fact(lfact.get('name'), lfact.get('value')))
                                rule.own_facts.append(newarr)
                else:                         # случай ||
                    for fact in facts:
                        arr = []
                        nest = fact.get('op')
                        for lfact in fact.get('operands'):

                            if nest == '&':
                                arr.append(Fact(lfact.get('name'), lfact.get('value')))
                            else:
                                arr.append(Fact(lfact.get('name'), lfact.get('value')))
                                rule.own_facts.append(arr)
                                arr = []

                        if nest == '&':
                            rule.own_facts.append(arr)


            else:
                arr = []
                if operation == "&":
                    for fact in facts:
                        arr.append(Fact(fact.get('name'), fact.get('value')))
                    rule.own_facts.append(arr)
                else:
                    for fact in facts:
                        rule.own_facts.append([Fact(fact.get('name'), fact.get('value'))])


            self.rules.append(rule)
