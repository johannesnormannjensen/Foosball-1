$(document).ready(
    function setPlayerGameNames() {
        for (var i = 0; i < 4; i++) {
            var a = document.getElementById("PlayerId");
            a.name = "PlayerGames[" + i + "].PlayerId";
            a.id = "PlayerGames[" + i + "].PlayerId";
        }
    }
);