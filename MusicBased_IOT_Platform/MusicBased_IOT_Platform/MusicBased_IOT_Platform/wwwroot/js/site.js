window.initScrollListener = () => {
    window.addEventListener("scroll", () => {
        const btn = document.querySelector(".scroll-to-top-btn");
        if (!btn) return;

        if (window.scrollY > 200) {
            btn.style.display = "block";
            btn.style.visibility = "visible";
        } else {
            btn.style.display = "none";
            btn.style.visibility = "hidden";
        }
    });
};

window.scrollToTop = () => {
    window.scrollTo({ top: 0, behavior: "smooth" });
};