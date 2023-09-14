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
import { MyProfile } from "../pages/MyProfile";
import { EditProfile } from "../pages/EditProfile";
import Muro from "../pages/Muro";

import { ComplainsProvider } from "../store/complainsContext";
import { UserContextProvider } from "../store/usrContext";
import { CommentsProvider } from "../store/commentsContext";
import { CommentsReceivedProvider } from "../store/commentsReceived";
import { FavoriteProvider } from "../store/favoritesContext";

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
    path: "/profile",
    element: <MyProfile />,
  },
  {
    path: "/editprofile",
    element: <EditProfile />,
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
    element: <Muro />,
  },
]);

export function AppRouter() {
  return (
    <UserContextProvider>
      <CommentsReceivedProvider>
        <FavoriteProvider>
          <CommentsProvider>
            <ComplainsProvider>
              <RouterProvider router={router} />
            </ComplainsProvider>
          </CommentsProvider>
        </FavoriteProvider>
      </CommentsReceivedProvider>
    </UserContextProvider>
  );
}
