truncate table Database1.[dbo].[User]

--���� ������ ������ ���� � ������� SSDT � ���� post deployment �������
INSERT INTO Database1.[dbo].[User] values(1, 'Ivan', '11111',  1, 'imail.ru'),(2, 'Petrov', '12324567',  3, 'p@gml.com'),(3, 'Sidorov', '22222',  2, 's@ml.ru'),(4, 'Popov', '123',  1, 'p@x.ru'),(5, 'Chirkova', '123',  2, 'c@ml.ru'),(6, 'Mihaylova', '125543',  3, 'm@yae.ru'),(7, 'Kolchin', '1I8Y623',  3, 'ko@ma.ru'),(8, 'Voron', '1231234554',  3, 'vo@ml.ru'),(9, 'Charikova', '1278753',  2, 'ch@l.com'),(10, 'Gluchova', '178768723',  3, 'g@l.ru');
select * from Database1.[dbo].[User];