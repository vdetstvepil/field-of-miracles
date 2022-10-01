--- Тестовый список вопросов для игры
--- Используется СУБД SQLite

--- Создание таблицы списка вопросов
CREATE VIRTUAL TABLE questions (id INTEGER AUTOINCREMENT PRIMARY KEY, level_number INTEGER, question_text VARCHAR(1000), variant_a VARCHAR(200), variant_b VARCHAR(200), variant_c VARCHAR(200), variant_d VARCHAR(200), variant_true VARCHAR(1));

--- Вставка вопросов в таблицу
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (1, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');