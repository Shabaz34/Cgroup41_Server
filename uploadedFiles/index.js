var Base64Code = "";
function encodeImageFileAsURL(element) {
  var file = element.files[0];
  var reader = new FileReader();
  reader.onloadend = function () {
    //console.log("RESULT", reader.result);
    Base64Code = reader.result;
  };
  reader.readAsDataURL(file);
}

function Test() {
  console.log(Base64Code);
  var imgArr = [];
  imgArr.push(Base64Code);

  const data = {
    api_key: "dMF9bxaLq8dcblg5CE5kGqgFKlJ63VYPF3cKD24u9p9KOenUxU",
    images: imgArr,
    /* modifiers docs: https://github.com/flowerchecker/Plant-id-API/wiki/Modifiers */
    modifiers: ["crops_fast", "similar_images"],
    plant_language: "en",
    /* plant details docs: https://github.com/flowerchecker/Plant-id-API/wiki/Plant-details */
    plant_details: [
      "common_names",
      "url",
      "name_authority",
      "wiki_description",
      "taxonomy",
      "synonyms",
    ],
  };

  let api = "https://api.plant.id/v2/identify";

  $.ajax({
    type: "POST",
    url: api,
    data: JSON.stringify(data),
    cache: false,
    contentType: "application/json",
    dataType: "json",
    success: successCB,
    error: errorCB,
  });
}

function successCB(data) {
  console.log(data);
}
function errorCB(err) {
  console.log(err);
}
