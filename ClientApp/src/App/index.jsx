import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import { useSelector } from "react-redux";


import { PublicRoutes, PrivateRoutes, AuthRouter, AppRouter } from "../router";
import Report from "../pages/Report/report";

export function App() {

  return (
    <BrowserRouter>
      <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/report" element={<Report />} />
        </Routes>
    </BrowserRouter>
  );
}
