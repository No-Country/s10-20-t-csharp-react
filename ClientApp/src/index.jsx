import React from "react";
import { createRoot } from "react-dom/client";
import "normalize.css";

import { App } from "./app";
import "./styles/index.css";
import { UserProvider } from "./store/userContext";

const rootElement = document.getElementById("root");
const root = createRoot(rootElement);

root.render( 
 <UserProvider>
   <App /> 
 </UserProvider>
 

);
