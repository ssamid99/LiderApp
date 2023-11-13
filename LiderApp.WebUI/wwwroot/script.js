/* Mobile Menu Open / Close */

const mobileMenu = document.querySelector(".mobile_menu");
const hamburger = document.querySelector(".hamburger");
const closeButton = document.querySelector(".close_button");

hamburger.addEventListener("click", function () {
  mobileMenu.style.width = "60%";
  mobileMenu.style.opacity = 1;
  mobileMenu.style.visibility = "visible";
});

closeButton.addEventListener("click", function () {
  mobileMenu.style.width = "0";
  mobileMenu.style.opacity = 0;
  mobileMenu.style.visibility = "hidden";
});

/* Header Mobile Dropdown */

const mobileDropdownMenuButton = document.querySelectorAll(
  ".mobileDropdownMenuButton"
);
const mobileDropdown = document.querySelectorAll(".mobile_dropdownMenu");

mobileDropdownMenuButton.forEach((button) => {
  button.addEventListener("click", function (e) {
    const currentIcon = e.target;
    const currentIconClass = e.target.classList[2];
    const currentDropdown = document.getElementById(e.target.dataset.id);

    const btnArr = Array.from(mobileDropdownMenuButton).filter(
      (btn) => btn.dataset.id !== e.target.dataset.id
    );

    const dropdownArr = Array.from(mobileDropdown).filter(
      (dropdown) => dropdown.id !== e.target.dataset.id
    );

    btnArr.forEach((btn) => {
      if (currentIconClass === "fa-plus" && btn.classList.contains("fa-plus")) {
        currentIcon.classList.replace("fa-plus", "fa-minus");
        currentDropdown.classList.add("mobile_dropdown-active");
      }

      if (
        currentIconClass === "fa-plus" &&
        btn.classList.contains("fa-minus")
      ) {
        currentIcon.classList.replace("fa-plus", "fa-minus");
        currentDropdown.classList.add("mobile_dropdown-active");

        btn.classList.replace("fa-minus", "fa-plus");

        dropdownArr.forEach((dropdown) => {
          dropdown.classList.remove("mobile_dropdown-active");
        });
      }

      if (currentIconClass === "fa-minus") {
        currentIcon.classList.replace("fa-minus", "fa-plus");
        currentDropdown.classList.remove("mobile_dropdown-active");
      }
    });
  });
});

/* FAQ */

const faqQuestionWrapperArr = Array.from(
  document.querySelectorAll(".faq_questionWrapper")
);

const faqButton = Array.from(document.querySelectorAll(".faq_button"));
const faqAnswer = Array.from(document.querySelectorAll(".faq_answer"));

faqQuestionWrapperArr.forEach((wrapper) => {
  wrapper.addEventListener("click", function (e) {
    const filteredArr = faqQuestionWrapperArr.filter(
      (answer) => answer.dataset.id !== this.dataset.id
    );

    const currentButton = this.childNodes[1].children[1];
    const currentButtonClass = currentButton.classList[2];
    const currentAnswer = this.childNodes[3];

    filteredArr.forEach((element) => {
      const wrapper = element.childNodes[1].children[1];

      if (
        currentButtonClass === "fa-plus" &&
        wrapper.classList.contains("fa-plus")
      ) {
        currentButton.classList.replace("fa-plus", "fa-minus");
        currentAnswer.classList.add("faq_answer-active");
      }

      if (
        currentButtonClass === "fa-plus" &&
        wrapper.classList.contains("fa-minus")
      ) {
        wrapper.classList.replace("fa-minus", "fa-plus");

        filteredArr.forEach((answer) => {
          answer.childNodes[3].classList.remove("faq_answer-active");
        });

        currentButton.classList.replace("fa-plus", "fa-minus");
        currentAnswer.classList.add("faq_answer-active");
      }

      if (currentButtonClass === "fa-minus") {
        currentButton.classList.replace("fa-minus", "fa-plus");
        currentAnswer.classList.remove("faq_answer-active");
      }
    });
  });
});

/* To fix navbar */

const navbar = document.getElementsByClassName("header_navbar_container")[0];

window.addEventListener("scroll", () => {
  window.scrollY > 300
    ? navbar.classList.add("header_navbar_container-fixed")
    : navbar.classList.remove("header_navbar_container-fixed");
});
