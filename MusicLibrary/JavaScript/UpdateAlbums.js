window.onload = function () {
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

            //grab artist name from the dropdown
            var artistName = $("#ArtistID :selected").text();
            var albumSuggest = "<a href = '/album/create?defaultArtistID=" + artistID + "'>" + "Add new " + artistName + " Album</a>";
            $("#AlbumSuggestion").html(albumSuggest);
        }
    );
    }
    else {
        var options = '<option value="1">---Select an Album---</option>';
        $("#AlbumID").html(options);
        var defaultAlbum = "<a href = '/album/create'>Add new Album</a>";
        $("#AlbumSuggestion").html(defaultAlbum);
    }
};

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

            //grab artist name from the dropdown
            var artistName = $("#ArtistID :selected").text();
            var albumSuggest = "<a href = '/album/create?defaultArtistID=" + artistID + "'>" + "Add new " + artistName + " Album</a>";
            $("#AlbumSuggestion").html(albumSuggest);
        }
    );
    }
    else
    {
        var options = '<option value="1">---Select an Album---</option>';
        $("#AlbumID").html(options);
        var defaultAlbum = "<a href = '/album/create'>Add new Album</a>";
        $("#AlbumSuggestion").html(defaultAlbum);
    }
});

