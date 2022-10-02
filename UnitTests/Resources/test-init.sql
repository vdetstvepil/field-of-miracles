--- Тестовый список вопросов для игры
--- Используется СУБД SQLite

--- Создание таблицы списка вопросов
CREATE TABLE questions (id INTEGER PRIMARY KEY, level_number INTEGER, question_text VARCHAR(1000), variant_a VARCHAR(200), variant_b VARCHAR(200), variant_c VARCHAR(200), variant_d VARCHAR(200), variant_true VARCHAR(1));

--- Создание таблицы статистики
CREATE TABLE statistics_table(id INTEGER PRIMARY KEY, nickname VARCHAR(30), score INTEGER);

--- Вставка вопросов в таблицу
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (1, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (2, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (3, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (4, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (5, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (6, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (7, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (8, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (9, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (10, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (11, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (12, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (13, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (14, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO questions (level_number, question_text, variant_a, variant_b, variant_c, variant_d, variant_true) VALUES (15, 'Тестовый вопрос', 'Ответ A', 'Ответ B', 'Ответ C', 'Ответ D', 'A');
INSERT INTO statistics_table(nickname, score) VALUES ('Anton', 5000);
INSERT INTO statistics_table(nickname, score) VALUES ('Zhenek', 1500000);
INSERT INTO statistics_table(nickname, score) VALUES ('Dashka', 3000000);
INSERT INTO statistics_table(nickname, score) VALUES ('Margo', 3000000);