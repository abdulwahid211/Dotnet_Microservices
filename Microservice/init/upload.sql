CREATE TABLE Users (
    ID int NOT NULL AUTO_INCREMENT,
    Name varchar(255),
    Description varchar(255),
    PRIMARY KEY (Id)
);

CREATE TABLE Posts (
    ID int NOT NULL AUTO_INCREMENT,
    Title varchar(255),
    Description varchar(255),
    UserID int NOT NULL,
    PRIMARY KEY (ID),
    FOREIGN KEY (UserID) REFERENCES Users(ID)

);