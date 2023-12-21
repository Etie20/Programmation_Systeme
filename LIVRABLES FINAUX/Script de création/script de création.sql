CREATE TABLE salle (
  id INT IDENTITY(1,1) PRIMARY KEY,
  chaises INT CHECK (chaises >= 0 AND chaises <= 10),
  corbeilles INT CHECK (corbeilles >= 0 AND corbeilles <= 10)
);

CREATE TABLE carres (
 id INT IDENTITY(1,1) PRIMARY KEY,
 salle_id INT,
 FOREIGN KEY (salle_id) REFERENCES salle(id)
);

CREATE TABLE rangs (
 id INT IDENTITY(1,1) PRIMARY KEY,
 carres_id INT,
 FOREIGN KEY (carres_id) REFERENCES carres(id)
);

CREATE TABLE tables (
 id INT IDENTITY(1,1) PRIMARY KEY,
 numero INT CHECK (numero >= 0 AND numero <= 10),
 capacite INT CHECK (capacite >= 0 AND capacite <= 10),
 rangs_id INT CHECK (rangs_id >= 0 AND rangs_id <= 10),
 FOREIGN KEY (rangs_id) REFERENCES rangs(id)
);

CREATE TABLE Ustensile (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255),
	Quantite INT CHECK (Quantite >= 0 AND Quantite <= 10),
	TempsNettoyage FLOAT CHECK (TempsNettoyage >= 0 AND TempsNettoyage <= 10)
);

CREATE TABLE TypeRecette (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255),
);

CREATE TABLE Recette (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Personnes INT CHECK (Personnes >= 0 AND Personnes <=10),
	Temps_prep FLOAT(10),
	TempsCuisson FLOAT(10),
	Temps_Pause INT CHECK (Temps_Pause >= 0 AND Temps_Pause <=10),
	TypeRecetteid INT NOT NULL,
	FOREIGN KEY (TypeRecetteid) REFERENCES TypeRecette(id)
);

CREATE TABLE Etape (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Ordre INT CHECK (Ordre >= 0 AND Ordre <=10),
	Temps FLOAT CHECK (Temps >= 0 AND Temps <=10),
	Description VARCHAR(255),	
	Recetteid INT NOT NULL,
	FOREIGN KEY(Recetteid) REFERENCES Recette(id)
);

CREATE TABLE EtapesUstensiles(
	Etapesid INT NOT NULL,
	Ustensilesid INT NOT NULL,
	FOREIGN KEY (Etapesid) REFERENCES Etape(id),
	FOREIGN KEY (Ustensilesid) REFERENCES Ustensile(id)
)

CREATE TABLE Appareil(
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255) NOT NULL,
	Quantite INT CHECK (Quantite >= 0 AND Quantite <= 10),
	Capacite INT CHECK (Capacite >= 0 AND Capacite <= 10),
)

CREATE TABLE TypeStockage(
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255) NOT NULL,
)
CREATE TABLE Denree(
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255) NOT NULL,
	Quantite INT NOT NULL,
	TypeStockageid INT NOT NULL,
	FOREIGN KEY (TypeStockageid) REFERENCES TypeStockage(id)
)
CREATE TABLE DenreeParEtape (
	Etapeid INT NOT NULL,
	Denreeid INT NOT NULL,
	FOREIGN KEY (Etapeid) REFERENCES Etape(id),
	FOREIGN KEY (Denreeid) REFERENCES Denree(id)
)

INSERT INTO Recette (Personnes, Temps_prep, TempsCuisson, Temps_Pause, TypeRecetteid) VALUES (1,5,5,5,1);
INSERT INTO Recette (Personnes, Temps_prep, TempsCuisson, Temps_Pause, TypeRecetteid) VALUES (6,5,5,5,2);
INSERT INTO Recette (Personnes, Temps_prep, TempsCuisson, Temps_Pause, TypeRecetteid) VALUES (10,5,5,5,3);
INSERT INTO Recette (Personnes, Temps_prep, TempsCuisson, Temps_Pause, TypeRecetteid) VALUES (8,5,5,5,1);


CREATE TABLE TypeAssiette (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL
);

INSERT INTO TypeAssiette (Name) VALUES ('STARTER');
INSERT INTO TypeAssiette (Name) VALUES ('BOWL');
INSERT INTO TypeAssiette (Name) VALUES ('DINNER');
INSERT INTO TypeAssiette (Name) VALUES ('DESSERT');
INSERT INTO TypeAssiette (Name) VALUES ('UNKNOW');

CREATE TABLE TypeVerre (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL
);

INSERT INTO TypeVerre (Name) VALUES ('WATER');
INSERT INTO TypeVerre (Name) VALUES ('VINE');
INSERT INTO TypeVerre (Name) VALUES ('FLUTE');
INSERT INTO TypeVerre (Name) VALUES ('UNKNOW');

CREATE TABLE TypeCouvert (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(255) NOT NULL
);

INSERT INTO TypeCouvert (Name) VALUES ('KNIFE');
INSERT INTO TypeCouvert (Name) VALUES ('FORK');
INSERT INTO TypeCouvert (Name) VALUES ('DESSERTSPOON');
INSERT INTO TypeCouvert (Name) VALUES ('TEASPOON');
INSERT INTO TypeCouvert (Name) VALUES ('UNKNOW');

INSERT INTO rangs (carres_id) VALUES (3);

INSERT INTO tables (numero, capacite, rangs_id) VALUES (4,8,4);
INSERT INTO tables (numero, capacite, rangs_id) VALUES (5,9,4);
INSERT INTO tables (numero, capacite, rangs_id) VALUES (6,7,4);

CREATE TABLE Assiettes (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255) NOT NULL,
	Quantite INT NOT NULL,
	TypeAssietteid INT NOT NULL,
	FOREIGN KEY (TypeAssietteid) REFERENCES TypeAssiette(id)
);

CREATE TABLE Couverts (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255) NOT NULL,
	Quantite INT NOT NULL,
	TypeCouvertid INT NOT NULL,
	FOREIGN KEY (TypeCouvertid) REFERENCES TypeCouvert(id)
);

CREATE TABLE Verres (
	id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255) NOT NULL,
	Quantite INT NOT NULL,
	TypeVerreid INT NOT NULL,
	FOREIGN KEY (TypeVerreid) REFERENCES TypeVerre(id)
);

INSERT INTO Assiettes (Nom, Quantite, TypeAssietteid) VALUES ('Assiette a entree',30,1);
INSERT INTO Assiettes (Nom, Quantite, TypeAssietteid) VALUES ('BOL',30,2);
INSERT INTO Assiettes (Nom, Quantite, TypeAssietteid) VALUES ('Assiette a dinner',30,3);
INSERT INTO Assiettes (Nom, Quantite, TypeAssietteid) VALUES ('Coupe dessert',30,4);
INSERT INTO Assiettes (Nom, Quantite, TypeAssietteid) VALUES ('inconnue',30,5);

INSERT INTO Couverts (Nom, Quantite, TypeCouvertid) VALUES ('Couteau',30,1);
INSERT INTO Couverts (Nom, Quantite, TypeCouvertid) VALUES ('Fourchette',30,3);
INSERT INTO Couverts (Nom, Quantite, TypeCouvertid) VALUES ('Cuillere a dessert',30,4);
INSERT INTO Couverts (Nom, Quantite, TypeCouvertid) VALUES ('Cuillere a the',30,5);
INSERT INTO Couverts (Nom, Quantite, TypeCouvertid) VALUES ('inconnue',30,6);

INSERT INTO Etape (Ordre, Temps, Description, Recetteid) VALUES (1, 5, 'Prechauffer au four', 1);
INSERT INTO Etape (Ordre, Temps, Description, Recetteid) VALUES (1, 5, 'Faire revenir la viande avec du beurre', 2);
INSERT INTO Etape (Ordre, Temps, Description, Recetteid) VALUES (1, 5, 'Etaler la pate dans un moule', 3);
INSERT INTO Etape (Ordre, iTemps, Description, Recetteid) VALUES (1, 5, 'Separer les blancs des jaunes', 4);

CREATE TABLE Etape_Appareil(
	Etapeid INT NOT NULL,
	Appareilid INT NOT NULL,
	FOREIGN KEY (Etapeid) REFERENCES Etape(id),
	FOREIGN KEY (Appareilid) REFERENCES Appareil(id)
)

INSERT INTO TypeStockage (Nom) VALUES ('AMBIANT');
INSERT INTO TypeStockage (Nom) VALUES ('COLD');
INSERT INTO TypeStockage (Nom) VALUES ('FREEZE');

INSERT INTO Appareil (Nom, Quantite, Capacite) VALUES ('Four', 1, 1);
INSERT INTO Appareil (Nom, Quantite, Capacite) VALUES ('poele', 1, 1);
INSERT INTO Appareil (Nom, Quantite, Capacite) VALUES ('feux de cuisson', 5, 1);
INSERT INTO Appareil (Nom, Quantite, Capacite) VALUES ('lave-vaisselle', 1, 10);
INSERT INTO Appareil (Nom, Quantite, Capacite) VALUES ('Machine_a_laver', 1, 10);
 
INSERT INTO Etape_Appareil (Etapeid, Appareilid) VALUES (1,1);


INSERT INTO Ustensile (Nom, Quantite, TempsNettoyage) VALUES ('Cuilleres_en_bois', 10, 1);
INSERT INTO Ustensile (Nom, Quantite, TempsNettoyage) VALUES ('Moule', 1, 1);
INSERT INTO Ustensile (Nom, Quantite, TempsNettoyage) VALUES ('Entonnoir', 1, 1);

INSERT INTO EtapesUstensiles (Etapesid, Ustensilesid) VALUES (2,1);
INSERT INTO EtapesUstensiles (Etapesid, Ustensilesid) VALUES (3,2);
INSERT INTO EtapesUstensiles (Etapesid, Ustensilesid) VALUES (4,3);


INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('Oeufs', 10, 1);
INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('Oignons', 10, 1);
INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('Sel', 10, 1);
INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('Poivre', 10, 1);
INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('Creme', 10, 3);
INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('chaire_crabe', 10, 3);
INSERT INTO Denree (Nom, Quantite, TypeStockageid) VALUES ('persil', 10, 2);



INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (1,15);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (2,15);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (3,16);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (3,17);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (3,18);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (3,19);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (4,20);
INSERT INTO DenreeParEtape (Etapeid, Denreeid) VALUES (4,21);












