import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import { useSelector } from "react-redux";
import { PublicRoutes, PrivateRoutes, AuthRouter, AppRouter } from "../router";
import Register from "../pages/Register";
import SignIn from "../pages/SignIn";
import Report from "../pages/Report/report";
import Muro from "../pages/Muro";
import { UserProvider } from "../store/userContext";

export function App() {

  return (
    <UserProvider>
            <BrowserRouter>
                <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/register" element={<Register />} />
                        <Route path="/login" element={<SignIn />} />
                        <Route path="/report" element={<Report />} />
                        <Route path="/muro" element={<Muro />} />
                  </Routes>
              </BrowserRouter>
    </UserProvider>
  );
}
