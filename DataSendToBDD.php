<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <title>Simulation Fourmilière</title>
</head>
<body>

<?php

//empeche une interuption d'execution due a un temps trop long
ini_set('max_execution_time', 0);

$table  = array(array());
$line = file(".//AntsWinForm//Fourmilliere.csv");

for ($i=0; $i<count($line); $i++) {
    $element = explode(" ",$line[$i]);
    
    for($j = 0; $j < count($element); $j++){
        $table[$i][$j] = $element[$j];
    }
}


//On prepare les futures connexion a la base
$user      = "root";
$password  = "";
$server    = "localhost";
$base      = "";
$connexion = new mysqli ($server, $user, $password, $base)
    or die(mysqli_connect_error());

    
// On créer la base et les tables si elle n'existe pas déja
$CreateDataBase = "CREATE DATABASE IF NOT EXISTS `Grp1Ant2022`";

if ($connexion -> query($CreateDataBase)=== TRUE){
    echo "<h4>La base de données à bien été créée</h2> ";
}
else {
    echo "<script> alert ('La base n'as pas été créée parceque j'en ai pas trop envie')</script>";
}
$connexion->select_db('Grp1Ant2022');
$CreateTable = "CREATE TABLE IF NOT EXISTS valueGrid(height int, width int, nbIteration int)";
$CreateTable2 = "CREATE TABLE IF NOT EXISTS donnee(content  varchar(25), sugar_amount int, nest_pheromone int, sugar_pheromone float)";
if ($connexion -> query($CreateTable)===TRUE){
    echo "<h4>La table valueGrid à bien été créée ou existe déja</h2> ";
}
else{
    echo "<script> alert ('Les tables n'ont pas été créée parceque j'en ai pas trop envie')</script>";
}
if ($connexion -> query($CreateTable2)===TRUE){
    echo "<h4>La table donnee à bien été créée ou existe déja</h2> ";
}
else{
    echo "<script> alert ('Les tables n'ont pas été créée parceque j'en ai pas trop envie')</script>";
}

//On commence par vider les deux tables
$requeteTruncate1 = "TRUNCATE TABLE valueGrid;";
$resultTruncate = $connexion->query($requeteTruncate1) or die($connexion->error);



$requeteTruncate2 = "TRUNCATE TABLE donnee;";
$resultTruncate = $connexion->query($requeteTruncate2) or die($connexion->error);

$connexion = new mysqli ($server, $user, $password, "Grp1Ant2022")
    or die(mysqli_connect_error());

echo "<h4>Les tables ont été vidée</h2> "; 


//On insert la premiere ligne dans la table valeurGrille pour recuperer la hauteur, largeur et le nb d'iteration
$height      = $table[0][0];
$width       = $table[0][1];
$nbIteration = $table[0][2];

$requeteInsert1  = "INSERT INTO valueGrid(height, width, nbIteration) VALUES(". $height .", ". $width .", ". $nbIteration .")";
echo ("<h4> Les données ont été insérée dans la table valueGrid</h2> ");

$connexion = new mysqli ($server, $user, $password, "Grp1Ant2022")
    or die(mysqli_connect_error());    




    
$resultInsert1 = $connexion->query($requeteInsert1) or die($connexion->error);



    //On insert les donnees dans la table
    $requeteInsert2  = "INSERT INTO donnee VALUES ";
    echo ("<h4> Les données ont été insérée dans la table donnee</h2> ");
    //On récupère la moitier des lignes arondie, soit 60201 lignes
    $maxLigne = round(count($table)/2);
    $i = 1;
    for ($i; $i < $maxLigne; $i++) { // 300 Tours * 400 lignes nécessaires à l'affichage + 1(l'en-tête)
        $col1 = $table[$i][0];
        $col2 = $table[$i][1];
        $col3 = $table[$i][2];
        $col4 = $table[$i][3];
        $requeteInsert2 .= "('". $col1 ."', ". $col2 .", ". $col3 .", ". $col4 .")";
        //Si on arrive à la dernière ligne de l'insersion, on n'ajoute pas de virgule à la fin
        if($i != $maxLigne -1){
            $requeteInsert2 .= ",";
        }
        //lorsqu'on arrive à la première moitier des lignes, on met à jour la valeurs maximum des lignes et ont         insert dans la bdd
        if($i ==  round(count($table)/2) -1){
            $maxLigne = count($table);
            $connexion->query($requeteInsert2) or die($connexion->error);
            $requeteInsert2  = "INSERT INTO donnee VALUES ";
        }
    }
    //Puis ont ajoute le reste des lignes dans la bdd
    $connexion->query($requeteInsert2) or die($connexion->error);
    echo ("<h4>Tout les informations ont été rentrée dans la bdd</h2>");
?>

</body>
</html>