const mainSlider = new Swiper(".swiper", {
  loop: true,
  autoplay: true,
  dots: true,
  pagination: {
    el: ".swiper-pagination",
  },

  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});

$(function () {
  var owl = $(".brends_sliderContainer");
  owl.owlCarousel({
    items: 6,
    margin: 10,
    loop: true,
    dots: false,
    autoplay: true,
    autoplaySpeed: 300,
    responsive: {
      0: {
        items: 2,
      },
      425: {
        items: 4,
      },
      768: {
        items: 5,
      },
      1024: {
        items: 6,
      },
    },
  });
});

$(function () {
  var owl = $(".repair_productsSlider");
  owl.owlCarousel({
    nav: true,
    items: 4,
    margin: 20,
    loop: true,
    dots: false,
    autoplay: true,
    responsive: {
      0: {
        items: 2,
        nav: false,
      },
      550: {
        items: 3,
      },
      768: {
        items: 3,
      },
      1024: {
        items: 4,
      },
    },
  });
});

$(function () {
  var owl = $(".service_sliderWrapper");
  owl.owlCarousel({
    nav: true,
    items: 5,
    margin: 20,
    loop: true,
    dots: true,
    autoplay: true,
    responsive: {
      0: {
        nav: false,
        dots: false,
        items: 2,
      },
      425: {
        nav: false,
        dots: false,
        items: 3,
      },
      768: {
        items: 4,
      },
      1024: {
        items: 5,
      },
    },
  });
});

$(function () {
  var owl = $(".feedback_slider");
  owl.owlCarousel({
    nav: true,
    items: 5,
    margin: 20,
    loop: true,
    dots: true,
    autoplay: true,
    responsive: {
      0: {
        items: 1,
      },
      425: {
        items: 2,
      },
      768: {
        items: 4,
      },
      1024: {
        items: 5,
      },
    },
  });
});
