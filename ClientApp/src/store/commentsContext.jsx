import axios from "axios";
import { createContext, useState } from "react";
import { useContext, useEffect } from "react";

function useCommentsSource() {
  const [comments, setComments] = useState([]);

  const token = localStorage.getItem("userSession");

  const config = {
    headers: { Authorization: `Bearer ${token}` },
  };

  useEffect(() => {
    axios
      .get("https://s10nc.somee.com/api/comments/left", config)
      .then(res => setComments(res.data.data))
      .catch(err => console.error(err));
  }, []);
  console.log(comments);

  return comments;
}

const CommentsContext = createContext();

export function useComments() {
  return useContext(CommentsContext);
}

export const CommentsProvider = ({ children }) => {
  return (
    <CommentsContext.Provider value={useCommentsSource()}>
      {children}
    </CommentsContext.Provider>
  );
};
