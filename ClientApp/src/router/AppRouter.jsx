import React from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";

// components
import Home from "../pages/home";
import Landing from "../pages/landing";
import { NotFound } from "../pages/404NotFound";
import SignIn from "../pages/SignIn";
import Register from "../pages/Register";
import Report from "../pages/report";
import MyReports from "../pages/MyReports";
import NewReport from "../pages/NewReport";
import Muro from "../pages/Muro";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Landing />,
  },
  {
    path: "/home",
    element: <Home />,
  },
  {
    path: "/login",
    element: <SignIn />,
  },
  {
    path: "/register",
    element: <Register />,
  },
  {
    path: "*",
    element: <NotFound />,
  },
  {
    path: "/report",
    element: <MyReports />,
  },
  {
    path: "/muro",
    element: <Muro/>,
  },
]);

export function AppRouter() {
  return <RouterProvider router={router} />;
}
