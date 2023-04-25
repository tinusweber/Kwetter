import React from "react";

import Postlist from "../../components/post/postlist/Postlist";
import Sidebar from "../../components/profilepage/sidebar/Sidebar";
import { useParams, useHistory } from "react-router-dom";
import './Profilepage.css'
import { useState, useEffect } from "react";
import UserService from '../../services/user.service'
export default function Profilepage() {
    let params = useParams();
    let history = useHistory();
  const [userDetails, setUserDetails] = useState({});
  useEffect(() => {
    UserService.getById(params.id).then(result => setUserDetails(result));
  }, [params.id])

  if (!userDetails) {
    history.push("/404")
  }

  return (
    
    <div>
      <div>
          <Sidebar userDetails={userDetails}/>
      </div>
      <div className="posts">
        <Postlist username={params.id}/>
      </div>
    </div>
  );
}
