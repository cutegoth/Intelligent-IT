import sys

from PyQt5.QtWidgets import QMainWindow, QMessageBox
from PyQt5 import QtWidgets
import main_ui
from explanation_component import ExplanationComponent
from inference_engine import InferenceEngine


class MainWindow(QMainWindow, main_ui.Ui_MainWindow):
    def __init__(self):
        super().__init__()
        self.setupUi(self)

        self.explanation_component = ExplanationComponent()
        self.restart()

        self.pushButton.clicked.connect(self.enter)
        self.pushButton_3.clicked.connect(self.restart)
        self.pushButton_4.clicked.connect(self.log)

    def enter(self):
        self.set_user_answer()
        self.inference_engine.get_answer_question()
        self.comboBox.clear()
        if self.inference_engine.isAnswer:
            self.label_2.setText('')
            self.message_box(self.inference_engine.question)
        else:
            self.label_2.setText(self.inference_engine.question)
            self.next()

    def message_box(self, answers):
        QMessageBox.about(self, "Title", answers)

    def set_user_answer(self):
        value = self.comboBox.currentText()
        if self.comboBox.currentText() == 'yes':
            value = True
        elif self.comboBox.currentText() == 'no':
            value = False

        self.inference_engine.set_user_answer(value)

    def restart(self):
        self.comboBox.clear()
        self.label_2.setText('')
        self.inference_engine = InferenceEngine()
        self.next()

    def log(self):
        logs = self.inference_engine.get_logs()
        self.message_box(logs)

    def next(self):
        self.label_2.setText(self.inference_engine.question)
        self.comboBox.addItems(self.inference_engine.answers)


if __name__ == '__main__':
    app = QtWidgets.QApplication(sys.argv)
    w = MainWindow()
    sys.exit(app.exec_())