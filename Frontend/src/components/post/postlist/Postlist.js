import React from 'react'
import Post from '../post/Post'
import { useState, useEffect } from 'react'
import PostService from '../../../services/post.service'
export default function Postlist({ username }) {
  const [posts, setPosts] = useState()
  const [loading, setLoading] = useState(true)
  useEffect(() => {
    if (username) {
      PostService.getPostByUserId(username).then((result) => {
        setPosts(result)
        setLoading(false)
      })
    } else {
      PostService.getPostsFromFollowing()
        .then((result) => {
          setPosts(result)
          setLoading(false)
        })
        .catch((er) => {})
    }
  }, [username])
  return !loading ? (
    <div>
      {posts?.map((post, idx) => (
        <Post
          key={idx}
          postId={post.id}
          imgData={post.imgData}
          subtitle={post.body}
          postedBy={post.tweeterId}
          datePosted={post.tweeterId}
        ></Post>
      ))}
    </div>
  ) : (
    <div data-testid='loading'>Loading...</div>
  )
}
