<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <link href="css/reset.css" rel="stylesheet" type="text/CSS">
    <link href="css/style.css" rel="stylesheet" type="text/CSS">
    <link rel="icon" type="image/png" href="favicon.ico"/>
    <meta http-equiv="X-UA-Compatible" content="IE=opera">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fourmiz</title>
</head>
<body>
    <header>
        <div id="Logo">
            <img src="Images/logo.png" alt="">
        </div>
    </header>
    <main>
        <div id="jeu">
            <div class="bouton">
                <button id="precedent" class="param" onclick="precedent()" >précédent</button>
                <div class="separateur"></div>
                <button id="suivant" class="param" onclick="suivant()" >suivant</button>
                <div class="separateur"></div>
                <button id="reset"  class="param" onclick="reset()" >réinitialiser</button>
            </div>
            <div id="plateau">
            <table>
                    <?php
                    
                        //Preparation pour les connexions
                        $user      = "root";
                        $password  = "";
                        $server    = "localhost";
                        $base      = "grp1ant2022";
                        $connnexion = new mysqli ($server, $user, $password, $base)
                            or die(mysqli_connect_error());    

                        //on fait une requête pour récuperer la hauteur et largeur du tableau prédéfini par la premiere ligne du fichier .csv
                        $sqlVal  = "SELECT * FROM valuegrid";
                        $resultVal = $connnexion->query($sqlVal) or die($connnexion->error);
                        //puis on les stock dans des variables qu'on utilise pour générer le tableau
                        while ($val = $resultVal->fetch_object()) {
                            $height = intval($val->height);
                            $width = intval($val->width);
                            $nbTours = $val->nbIteration;
                        }

                        //Initialisation du tableau vide
                        $case = " ";
                        echo "<table border='1'>";
                        for ($i=0; $i<$height; $i++){
                            echo "<tr>";
                            for($j = 0; $j<$width; $j++){
                                echo "<td>" . $case . "</td>";
                            }
                            echo "</tr>";
                        }
                        echo "</table>";
                        //requete à la bado
                        $sql  = "SELECT * FROM donnee";
                        $result = $connnexion->query($sql) or die($connnexion->error);

                        //ici, on prepare le tableau rempli qu'on recuperera en javascript sous forme de chaine de caractere
                        $tabCase = array();
                        $i=0;
                        while ($ligne = $result->fetch_object()) {
                            $tabCase[$i] = $ligne->content . ",". $ligne->sugar_amount .",". $ligne->nest_pheromone .",". $ligne->sugar_pheromone;
                            $i++;
                        }
                    ?>
                    <script>
                        let tabCase = document.querySelectorAll("td");
                        //on initialise le tableau en js et on le rempli directement en separant les elements de la chaine de caractere 
                        <?php echo "let tab = '".implode("<>", $tabCase)."'.split('<>');"; ?>
                        // alert(tab);

                        let vraiTab  = [];      //tableau double entree                      
                        let splitTab = [];      //tableau tampon
                        for(let i=0; i<tab.length; i++) {
                            //on split chaque element du tableau car on ne souhaitera afficher que le premier caractere
                            //une fourmis, le nid, rien du tout ...
                            splitTab = tab[i].split(','); //[F,0,0,0] devient [F][0][0][0]
                            vraiTab[i] = splitTab;
                        }
                        


                        let height = parseInt("<?php echo $height ?>");
                        let width = parseInt("<?php echo $width ?>");
                        sizeTab = height*width;

                        let init = 0;
                        //console.log(vraiTab);
                        
                        function afficheTourSuivant() {
                            if (init+sizeTab <= vraiTab.length) {
                                //appel fonction d'affichage de la grille
                                affichage(init, init+sizeTab);
                                init+=sizeTab;
                            } else {
                                window.clearInterval(tourActuel);
                                alert("Simulation terminée");
                            }
                        }
                        function afficheTourPrecedent() {
                            if (init > sizeTab) {
                                init-=sizeTab;
                                //appel fonction d'affichage de la grille
                                affichage(init-sizeTab, init);
                            } else {
                                alert("Vous ne pouvez pas revenir en arrière");
                            }
                        }

                        function affichage(vMin, vMax) {
                            let j=0;
                            for (let i=vMin; i<vMax; i++) {
                                //on ecrit que le premier caractere dans la case
                                if (vraiTab[i][0] == "F") {
                                    tabCase[j].textContent = "";
                                    tabCase[j].style.background = "#1e1e1e";
                                } else if (vraiTab[i][0] == "a"){
                                    tabCase[j].textContent = "";
                                    tabCase[j].style.background = 'url("Images/Fourmi.png")';
                                    tabCase[j].style.backgroundSize = "contain";
                                    tabCase[j].style.backgroundRepeat = "no-repeat";
                                    tabCase[j].style.backgroundPosition = "center";
                                }  else if (vraiTab[i][0] == "A"){
                                    tabCase[j].textContent = "";
                                    tabCase[j].style.background = 'url("Images/FourmiSucre.png")';
                                    tabCase[j].style.backgroundSize = "contain";
                                    tabCase[j].style.backgroundRepeat = "no-repeat";
                                    tabCase[j].style.backgroundPosition = "center";
                                } else if (vraiTab[i][0] == "X"){
                                    tabCase[j].textContent = "";
                                    tabCase[j].style.background = 'url("Images/Caillou.png")';
                                    tabCase[j].style.backgroundSize = "contain";
                                    tabCase[j].style.backgroundRepeat = "no-repeat";
                                    tabCase[j].style.backgroundPosition = "center";
                                } else if (vraiTab[i][0] == "S"){
                                    tabCase[j].textContent = "";
                                    tabCase[j].style.background = 'url("Images/Sucre.png")';
                                    tabCase[j].style.backgroundSize = "contain";
                                    tabCase[j].style.backgroundRepeat = "no-repeat";
                                    tabCase[j].style.backgroundPosition = "center";
                                } else if (vraiTab[i][0] == "N"){
                                    tabCase[j].textContent = "";
                                    tabCase[j].style.background = 'url("Images/Nid.png")';
                                    tabCase[j].style.backgroundSize = "contain";
                                    tabCase[j].style.backgroundRepeat = "no-repeat";
                                    tabCase[j].style.backgroundPosition = "center";
                                }else {
                                    tabCase[j].textContent = vraiTab[i][0];
                                    tabCase[j].style.background = "white";
                                }
                                if (vraiTab[i][3] > 8) {
                                    if (vraiTab[i][0]=="F"){
                                        tabCase[j].style.background = 'url("Images/Rouge.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                    else if (vraiTab[i][0]=="a"){
                                        tabCase[j].style.background = 'url("Images/FourmiRouge.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                    else if (vraiTab[i][0]=="A"){
                                        tabCase[j].style.background = 'url("Images/FourmiRougeSUCRE.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                }

                                else if (vraiTab[i][3] > 5) {
                                    if (vraiTab[i][0]=="F"){
                                        tabCase[j].style.background = 'url("Images/Orange.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                    else if (vraiTab[i][0]=="a"){
                                        tabCase[j].style.background = 'url("Images/FourmiOrange.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                    else if (vraiTab[i][0]=="A"){
                                        tabCase[j].style.background = 'url("Images/FourmiOrangeSUCRE.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                } 

                                else if (vraiTab[i][3] > 3) {
                                    if (vraiTab[i][0]=="F"){
                                        tabCase[j].style.background = 'url("Images/Jaune.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                    else if (vraiTab[i][0]=="a"){
                                        tabCase[j].style.background = 'url("Images/FourmiJaune.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";
                                    }
                                    else if (vraiTab[i][0]=="A"){
                                        tabCase[j].style.background = 'url("Images/FourmiJauneSUCRE.png';
                                        tabCase[j].style.backgroundSize = "contain";
                                        tabCase[j].style.backgroundRepeat = "no-repeat";
                                        tabCase[j].style.backgroundPosition = "center";

                                    }
                                } 


                                j++;
                            }
                        }
                        
                        //affichage initial
                        afficheTourSuivant();
                        
                        //appelle les fonctions lors des clicks sur les boutons 
                        function suivant()   { afficheTourSuivant();   }
                        function precedent() { afficheTourPrecedent(); }

                        let boolStart = true;
                        function start() {
                            if (boolStart) {
                                boolStart = false;
                                //automatisation des tours + on stocke le tour
                                tourActuel = window.setInterval(afficheTourSuivant, 500);
                            }
                        }
                        function pause() {
                            boolStart = true;
                            //met pause a la simulation
                            window.clearInterval(tourActuel);
                        }
                        function reset() {
                            window.clearInterval(tourActuel);
                            //on remet l'index de depart du tour à 0
                            init = 0;
                            //et on reinitialise l'affichage
                            afficheTourSuivant();
                            boolStart = true;
                        }
                    </script>
            </div>
            <div class="bouton">
                <button id='Start' class="param" onclick="start()" >Start</button>
                <div class="separateur"></div>
                <button id='Pause' class="param" onclick="pause()">Pause</button>
            </div>
        </div>
    </main>
    <footer>
        <div id="CR">
            <img class="copyright" src="Images/C.png" alt="">
            <p>copyright<br>all right reserved</p>
        </div>
    </footer>
</body>
</html>