CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

-- STUB restaurants

CREATE TABLE restaurants(
id INT NOT NULL AUTO_INCREMENT PRIMARY KEY, 
name VARCHAR(255) NOT NULL,
description TEXT NOT NULL,
imgUrl VARCHAR(500) NOT NULL,
exposure INT NOT NULL DEFAULT 0,
creatorId VARCHAR(255) NOT NULL,

FOREIGN KEY (creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

SHOW CREATE TABLE restaurants;
ALTER TABLE restaurants DROP FOREIGN KEY restaurants_ibfk_1;
ALTER TABLE restaurants ADD FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE;

ALTER TABLE restaurants
ADD COLUMN shutdown BOOLEAN NOT NULL DEFAULT false;

DROP TABLE restaurants;

INSERT INTO restaurants
(name, imgUrl, creatorId, description)
VALUES
('Big B Cheese', 'https://s3-alpha-sig.figma.com/img/871b/04bd/64cb8adb72ca7fe0b48e23b831a96ec1?Expires=1664755200&Signature=OydC7SxmGmSXlkhNsKGVxBG7K5hcEBwVOR-yZGel9eNcMttB~oa7M8LPBNtf0z8yPiVWFs5CO6BL-4FK5oQ6ola-~LnLvXzOm~At6e3tsEsX4A8OjuQXwRYjHOIzX7DlLd~h0-8XN4Lp~17G6UtMsAYWDIBVgRuPShlReqLq--zCJjjpa0y3~VhMTbr4-whKiJfGmRagjsqRrU8tLWvkzHfwwXjQxZvqrlrEpeYksiWboKw9fxY1wvKO6P4ti3ZF~dsbjZfrtCXZP1QeM4bsMjVdYggGa-43-ho0EZbOoBjoA00FC2HKMYhQH6XZsN-0Z-7NgU3yuLbO~jz9ZS-YDQ__&Key-Pair-Id=APKAINTVSUGEWH5XD5UA', '631b5b5fa7f0b66bb817725a', 'Bring the whole family for a BIG TIME fun!');
INSERT INTO restaurants
(name, imgUrl, creatorId, description)
VALUES
('Wild Dog Doggies', 'https://www.boredpanda.com/blog/wp-content/uploads/2020/11/raccoons-hot-dogs-james-blackwood-nova-scotia-coverimage.jpg', '632cc248c1fe0f9df71b9d4d', "We specialize in hot dogs for wild dogs.  We are a charity Restaurant and do not want anyone to give us a bad rating cause they expected us to feed their hugry kids after they got all hopped up on some 'white bottle' 'candies' some guy named Miles hanging outside the Big B Cheese.");

SELECT
rest.*,
COUNT(rep.id) AS reportCount,
a.*
FROM restaurants rest
LEFT JOIN reports rep ON rep.restaurantId = rest.id 
JOIN accounts a ON rest.creatorId = a.id
GROUP BY(rest.id)
;

    SELECT
        rest.*,
        COUNT(rep.id) AS reportCount,
        a.*
    FROM restaurants rest
    LEFT JOIN reports rep ON rep.restaurantId = rest.id 
    JOIN accounts a ON rest.creatorId = a.id
    GROUP BY(rest.id)
    WHERE rest.id = 1;

       SELECT
        rest.*,
        COUNT(rep.id) AS reportCount,
        a.*
    FROM restaurants rest
    LEFT JOIN reports rep ON rep.restaurantId = rest.id 
    JOIN accounts a ON rest.creatorId = a.id
    GROUP BY(rest.id)
    ORDER BY rest.exposure desc;

-- NOTE special thing for this project and resetting exposures
-- NOTE to enable events
SET GLOBAL event_scheduler = on;

-- reset exposure after each minute
CREATE EVENT reset_restaurant_exposure
ON SCHEDULE AT CURRENT_TIMESTAMP + INTERVAL 1 MINUTE
DO
  UPDATE restaurants SET exposure = 0;

  DROP EVENT IF EXISTS reset_restaurant_exposure;


-- STUB reports
CREATE TABLE reports(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(150) NOT NULL,
  rating INT NOT NULL DEFAULT 1,
  body TEXT,
  creatorId VARCHAR(255) NOT NULL,
  restaurantId INT NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY (restaurantId) REFERENCES restaurants(id)ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE reports;

INSERT INTO reports
(title, rating, creatorId, restaurantId, body)
VALUES
('The Rat did not impress', 4, '6216b36ebc31a249987812b1', 7, 'The Rat came out to our birthday party for my daughter, he did a weird dance, ate a slice of our `BCP` (big cheese pizza), hit us with a peace sign and i quote "Deuces"');