import {
  useState,
  createContext,
  useEffect,
  useReducer,
  useContext,
} from "react";
import axios from "axios";

export const ComplainsContext = createContext([]);

export const useComplainsSource = () => {
  const [complains, dispatch] = useReducer((state, action) => {
    switch (action.type) {
      case "setComplains":
        return { ...state, complains: action.payload };
      case "setTitle":
        return { ...state, title: action.payload };
      case "setText":
        return { ...state, text: action.payload };
      case "setLongitude":
        return { ...state, longitude: action.payload };
      case "setLatitude":
        return { ...state, latitude: action.payload };
      case "setVideoAddress":
        return { ...state, videoAddress: action.payload };
      case "setPhotoAddress":
        return { ...state, photoAddress: action.payload };
      case "setuserName":
        return { ...state, userName: action.payload };
      case "setUserPhoto":
        return { ...state, userPhoto: action.payload };
      case "setAddress":
        return { ...state, address: action.payload };
      case "setLikesCount":
        return { ...state, likesCount: action.payload };
    }
  }, []);

  useEffect(() => {
    axios.get("https://s10nc.somee.com/api/quejas").then(res => {
      console.log(res.data.data);
      dispatch({
        type: "setComplains",
        payload: res.data.data,
      });
    });
  }, []);

  return complains;
};

export const useComplains = () => {
  return useContext(ComplainsContext);
};

export const ComplainsProvider = ({ children }) => {
  return (
    <ComplainsContext.Provider value={useComplainsSource()}>
      {children}
    </ComplainsContext.Provider>
  );
};
