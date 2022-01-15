

class ExplanationComponent:

    def get_logs(self, facts):
        logs = []
        for fact in facts:
            log = '{0} ----> {1}'.format(fact.name, fact.value)
            logs.append(log)

        return '\n'.join(logs)