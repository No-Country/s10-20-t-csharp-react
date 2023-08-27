/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        poppins: ["Poppins", "sans-serif"],
      },
      fontSize: {
        '2xl': "3rem"
      },
      colors: {
        white: "#ffffff",
        "bg-color": "#fff",
        "gray-50": "#ebebeb",
        "gray-100": "#999ca0",
        "gray-150": "#656565",
        "primary-50": "#e8f0fe",
        "primary-100": "#184656",
        "primary-150": "#5c9ce8",
        "secondary-50": "#a4d0c0",
        "secondary-100": "#488c73",
        success: "",
        error: "#ff4e64",
      },
      borderRadius: {
        '2xl': "20px"
      }
    },
  },
  plugins: [],
};
