//In the song edit screen, this populates the dropdown for the song's artist.

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
            })
        }
    });