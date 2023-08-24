import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import { useSelector } from "react-redux";

import { PublicRoutes, PrivateRoutes, AuthRouter, AppRouter } from "../router";
import Register from "../pages/Register";
import SignIn from "../pages/SignIn";

export function App() {

  return (
    <BrowserRouter>
      <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/register" element={<Register />} />
              <Route path="/login" element={<SignIn />} />

        </Routes>
    </BrowserRouter>
  );
}
