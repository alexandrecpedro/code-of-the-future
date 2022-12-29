-- PART 1: INSERT
-- (A) BEFORE INSERT
delimiter //
CREATE TRIGGER person_bi BEFORE INSERT
ON person
FOR EACH ROW
IF NEW.age < 18 THEN
SIGNAL SQLSTATE '50001' SET MESSAGE_TEXT = 'Person must be older than 18.';
END IF; //
delimiter ;

-- (B) AFTER INSERT
delimiter //
CREATE TRIGGER person_ai AFTER INSERT
ON person
FOR EACH ROW
UPDATE average_age SET average = (SELECT AVG(age) FROM person); //
delimiter ;

-- PART 2: UPDATE
-- (A) BEFORE UPDATE
delimiter //
CREATE TRIGGER person_bu BEFORE UPDATE
ON person
FOR EACH ROW
IF NEW.age < 18 THEN
SIGNAL SQLSTATE '50002' SET MESSAGE_TEXT = 'Person must be older than 18.';
END IF; //
delimiter ;

-- (B) AFTER UPDATE
delimiter //
CREATE TRIGGER person_au AFTER UPDATE
ON person
FOR EACH ROW
UPDATE average_age SET average = (SELECT AVG(age) FROM person); //
delimiter ;

-- PART 3: DELETE
-- (A) BEFORE DELETE
delimiter //
CREATE TRIGGER person_bd BEFORE DELETE
ON person
FOR EACH ROW
INSERT INTO person_archive (name, age)
VALUES (OLD.name, OLD.age); //
delimiter ;

-- (B) AFTER DELETE
delimiter //
CREATE TRIGGER person_ad AFTER DELETE
ON person
FOR EACH ROW
UPDATE average_age SET average = (SELECT AVG(person.age) FROM person); //
delimiter ;

-- PART 4: MULTIPLES TRIGGERS
CREATE TRIGGER <trigger name> <trigger time > <trigger event>
ON <table name>
FOR EACH ROW
BEGIN
<trigger body>;
END;

-- PART 5: SHOW TRIGGERS
SHOW triggers;