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
        "black": "#0C0C0D",
        "primary-50": "#F8D36F",
        "primary-100": "#F7BD22",
        "secondary-50": "#ACD299",
        "secondary-100": "#92AF43",
        "terciary-50": "#7CACE9",
        "terciary-100": "#1E3756",
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
