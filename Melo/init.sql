DROP TABLE IF EXISTS videos;
DROP TABLE IF EXISTS pictures;
DROP TABLE IF EXISTS musics;
DROP TABLE IF EXISTS directories;


CREATE TABLE directories
( id INT IDENTITY(1,1) PRIMARY KEY,
  name VARCHAR(64) NOT NULL,
  directory_path TEXT NOT NULL
);

CREATE TABLE videos
( id INT IDENTITY(1,1) PRIMARY KEY,
  directory_id INT NOT NULL,
  file_path TEXT NOT NULL,
  name VARCHAR(64) NOT NULL,
  extension VARCHAR(8) NOT NULL,
  comment TEXT,
  CONSTRAINT fk_videos_directory
  FOREIGN KEY (directory_id)
  REFERENCES directories (id)
  ON DELETE CASCADE
);

CREATE TABLE pictures
( id INT IDENTITY(1,1) PRIMARY KEY,
  directory_id INT NOT NULL,
  file_path TEXT NOT NULL,
  name VARCHAR(64) NOT NULL,
  extension VARCHAR(8) NOT NULL,
  comment TEXT,
  CONSTRAINT fk_pictures_directory
  FOREIGN KEY (directory_id)
  REFERENCES directories (id)
  ON DELETE CASCADE
);

CREATE TABLE musics
( id INT IDENTITY(1,1) PRIMARY KEY,
  directory_id INT NOT NULL,
  file_path TEXT NOT NULL,
  name VARCHAR(64) NOT NULL,
  extension VARCHAR(8) NOT NULL,
  artist VARCHAR(32),
  album VARCHAR(32),
  title VARCHAR(32),
  comment TEXT,
  CONSTRAINT fk_musics_directory
  FOREIGN KEY (directory_id)
  REFERENCES directories (id)
  ON DELETE CASCADE
);
