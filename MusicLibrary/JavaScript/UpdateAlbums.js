//On load, album add suggestion and genre fields are changed.
window.onload = function () {
    var artistID = $("#ArtistID").val();
    var albumID = $("#AlbumID").val();
    var foundAlbumGenre;
    
    //If an artist is preselected, run this
    if (artistID != "") {
        $.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
            for (var z = 0; z < albums.AlbumNames.length; z++) {
                if (albums.AlbumNames[z].AlbumID == albumID) {
                    foundAlbumGenre = albums.AlbumNames[z].GenreName;
                    break;
                }
            }
            //use artist name from the dropdown to suggest creating a new album for that artist
            var artistName = $("#ArtistID :selected").text();
            var albumSuggest = "<a href = '/album/newsongcreate?defaultArtistID=" + artistID + "'>" + "Add new " + artistName + " Album</a>";
            $("#AlbumSuggestion").html(albumSuggest);
            $("#GenreID").html(foundAlbumGenre);
        });
    }
    //If no artist is preselected, leave Genre blank
    else
        $("#GenreID").html("");
}
//populates new album list and genre when you change the artist name from the dropdown.
$("#ArtistID").change(function () {
    var foundAlbum;
    var artistID = $("#ArtistID").val();
    if (artistID != "") {
        $.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
            var options = "";
            //populate albumname dropdown
            for (var z = 0; z < albums.AlbumNames.length; z++) {
                var albumID = albums.AlbumNames[z].AlbumID;
                var albumName = albums.AlbumNames[z].AlbumName;
                options += '<option value ="' + albumID + '">' + albumName + '</option>'
            }
            $("#AlbumID").html(options);
            //grab the current album and change the Genre parameter.
            if ($("#AlbumID").val() != null) {
                var albumSelected = parseInt($("#AlbumID").val(), 10);
                $.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
                    for (var z = 0; z < albums.AlbumNames.length; z++) {
                        if (albums.AlbumNames[z].AlbumID == albumSelected) {
                            foundAlbum = albums.AlbumNames[z].GenreName;
                            break;
                        }
                    }
                    $("#GenreID").html(foundAlbum);
                });
            }
            else
                $("#GenreID").html("");


            //suggest adding a new album from the selected artist.
            var artistName = $("#ArtistID :selected").text();
            var albumSuggest = "<a href = '/album/newsongcreate?defaultArtistID=" + artistID + "'>" + "Add new " + artistName + " Album</a>";
            $("#AlbumSuggestion").html(albumSuggest);
        }
    );
    }
            //When user selects the "---Select an Artist---" value from the dropdown.
    else
    {
        var options = '<option value="1">---Select an Album---</option>';
        $("#AlbumID").html(options);
        var defaultAlbum = "<a href = '/album/newsongcreate'>Add new Album</a>";
        $("#AlbumSuggestion").html(defaultAlbum);
        $("#GenreID").html("");
    }
});

//update genre when you change the album.
$("#AlbumID").change(function () {
    var artistID = $("#ArtistID").val();
    var albumSelected = $("#AlbumID").val();
    if (albumSelected != "") {
        var foundAlbum;
        $.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
            for (var z = 0; z < albums.AlbumNames.length; z++) {
                if (albums.AlbumNames[z].AlbumID == albumSelected) {
                    foundAlbum = albums.AlbumNames[z].GenreName;
                    break;
                }
            }

            $("#GenreID").html(foundAlbum);
        })
    }
    else
        $("#GenreID").html("");
});

