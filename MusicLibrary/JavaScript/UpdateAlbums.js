$("#ArtistID").change(function () {
    var artistID = $("#ArtistID").val();

$.get("/songs/UpdatedAlbums", { artistID: artistID }, function (albums) {
    console.log("This is great");
    var options = "";
    for (var z = 0; z < albums.AlbumNames.length; z++) {
        var albumID = albums.AlbumNames[z].AlbumID;
        var albumName = albums.AlbumNames[z].AlbumName;
        options += '<option value ="' + albumID + '">' + albumName + '</option>'
    }

    $("#AlbumID").html(options);
}
);
});
