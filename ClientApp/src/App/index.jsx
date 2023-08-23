import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import { useSelector } from "react-redux";

import { PublicRoutes, PrivateRoutes, AuthRouter, AppRouter } from "../router";

export function App() {

  return (
    <BrowserRouter>
      <Routes>
              <Route path="/" element={<Home />} />
        </Routes>
    </BrowserRouter>
  );
}
