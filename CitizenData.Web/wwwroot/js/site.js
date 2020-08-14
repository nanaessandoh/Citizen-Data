
// Add the following code if you want the name of the file appear on select
    $(".custom-file-input").on("change", function() {
  var fileName = $(this).val().split("\\").pop();
  $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });


// Fade/Slide Bootstap Alert
$("#bootstrap-alert").fadeTo(5000, 1000).slideUp(1000, function () {
    $("#bootstrap-alert").slideUp(1000);
});



