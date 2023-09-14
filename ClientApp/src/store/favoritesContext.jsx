import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";

function useLikeSource() {
  const [likes, setLikes] = useState([]);
  useEffect(() => {
    axios
      .get("https://s10nc.somee.com/api/me/favorites", {
        withCredentials: true,
      })
      .then(res => setLikes(res.data.data));
    console.log(likes);
  }, []);

  return likes;
}

const LikeContext = createContext();

export function useFavorites() {
  return useContext(LikeContext);
}

export const FavoriteProvider = ({ children }) => {
  return (
    <LikeContext.Provider value={useLikeSource()}>
      {children}
    </LikeContext.Provider>
  );
};
