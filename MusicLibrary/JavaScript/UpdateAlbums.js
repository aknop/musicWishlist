//After adding an album or artist directly from the song edit screen, this will make sure the album suggestion link will show that artist's name
window.onload = function () {
    var artistID = $("#ArtistID").val();
    if (artistID != "") {
        $.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
            //use artist name from the dropdown to suggest creating a new album for that artist
            var artistName = $("#ArtistID :selected").text();
            var albumSuggest = "<a href = '/album/newsongcreate?defaultArtistID=" + artistID + "'>" + "Add new " + artistName + " Album</a>";
            $("#AlbumSuggestion").html(albumSuggest);
        });
    };
}
//populates new album list when you change the artist name from the dropdown.
    $("#ArtistID").change(function () {
        var artistID = $("#ArtistID").val();
        if (artistID != "") {
            $.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
                var options = "";
                for (var z = 0; z < albums.AlbumNames.length; z++) {
                    var albumID = albums.AlbumNames[z].AlbumID;
                    var albumName = albums.AlbumNames[z].AlbumName;
                    options += '<option value ="' + albumID + '">' + albumName + '</option>'
                }

                $("#AlbumID").html(options);

                //grab artist name from the dropdown and suggest adding a new album from them.
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
        }
    });

