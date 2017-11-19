
CREATE TABLE Drink(
    Event_ID int IDENTITY(1,1) NOT NULL,
    beverage varchar(30) NOT NULL,
    time_start time default (CURRENT_TIMESTAMP),
    time_finish time,
    make varchar(30), /*NA: Scrap this. Removed from spec.*/
    model varchar(30),
    alcohol_percentage int NOT NULL, /*NA:Does this need to be an int or can it be a float/double?*/
    Container int, /*NA: Should this be a string?*/
    v_percentage int, /*NA: This means percentage consumed?*/
    volume_reading int, /*NA: What is this?*/
    PRIMARY KEY(Event_ID)
);

Create TABLE Event(
    ID int,
    Created_timestamp TIMESTAMP,
    FOREIGN KEY (ID) REFERENCES Drink(Event_ID)
);





