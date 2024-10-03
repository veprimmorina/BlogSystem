# Blog System

**Project Goal:**  
To build a fully-featured blog platform that allows users to create, manage, and interact with blog posts through comments, likes, and search functionality.

---

### Technologies:
- **C#** (backend)
- **ASP.NET Core** (web framework)
- **Entity Framework** (ORM for database operations)
- **SQL Server** (database)

---

### Key Features:
- **Blog Post Management**  
   Users with specific roles can create, edit, and delete blog posts. Administrators have full control, while Creators have limited permissions.

- **Search and Filter**  
   The application provides powerful search and filtering options for blog posts, allowing users to search by title, tags, and date range.

- **Like System**  
   Users can like blog posts, increasing engagement and visibility. The like service is integrated seamlessly within the platform.

- **Commenting and Reply System**  
   Users can add comments to blog posts, and both comments and replies can be created, viewed, and managed.

---

### User Roles:
- **Administrator**  
   - Full control over the entire platform.
   - Can manage all blog posts and comments.
   - Able to search and filter posts, view all data, and delete content.
   
- **Creator**  
   - Limited to creating, updating, and deleting their own blog posts.
   - Can interact with their posts through comments and likes.

---

### API Endpoints:

- **Blog Post Management**
   - `POST /api/BlogPosts`  
     Create a new blog post (requires Administrator or Creator role).
   
   - `GET /api/BlogPosts`  
     Get a list of all blog posts (Administrator only).
   
   - `PUT /api/BlogPosts/{postId}`  
     Update a blog post by ID (Administrator or Creator).
   
   - `DELETE /api/BlogPosts/{postId}`  
     Delete a blog post by ID (Administrator or Creator).
   
   - `GET /api/BlogPosts/{id}`  
     Get details of a specific blog post by ID (Administrator only).

- **Search and Filter**
   - `GET /api/BlogPosts/search`  
     Search blog posts by title (available to all users).
   
   - `GET /api/BlogPosts/filter`  
     Filter blog posts by date range and tags (available to all users).

- **Likes and Comments**
   - `GET /api/BlogPosts/like?postId={postId}`  
     Add a like to a blog post (available to all users).
   
   - `POST /api/BlogPosts/createComment`  
     Add a comment to a blog post (available to all users).
   
   - `GET /api/BlogPosts/getAll`  
     Get all comments on blog posts (available to all users).
   
   - `POST /api/BlogPosts/reply`  
     Add a reply to a comment (available to all users).

---

### Advantages of the Application:
- **Role-Based Access Control**  
   Fine-grained permissions allow only authorized users to perform specific actions.
   
- **Interactive Blog Platform**  
   Engages users with comments and likes, fostering community interaction.

- **Scalable and Maintainable**  
   Follows best practices in API development, ensuring the app can be extended easily in the future.

---

