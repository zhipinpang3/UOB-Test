CREATE TABLE Members (
    Id BIGINT PRIMARY KEY AUTO_INCREMENT,
    Fullname VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE,
    PhoneNumber VARCHAR(20)
);


CREATE TABLE Books (
    Id BIGINT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Genre VARCHAR(100),
    IsAvailable BOOLEAN NOT NULL
);


CREATE TABLE Loans (
    BookId BIGINT NOT NULL,
    MemberId BIGINT NOT NULL,
    LoanDate DATETIME2 NULL,
    DueDate DATETIME2 NULL,
    ReturnDate DATETIME2 NULL,
    FineAmount DECIMAL(10, 2) NULL,
    CONSTRAINT PK_Loans PRIMARY KEY (BookId, MemberId),
    CONSTRAINT FK_Loans_BookId FOREIGN KEY (BookId) REFERENCES Books(BookId),
    CONSTRAINT FK_Loans_MemberId FOREIGN KEY (MemberId) REFERENCES Members(MemberId)
);