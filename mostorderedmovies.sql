Select MovieId,Title,count(rmid) as ordertimes from Movies, rentedmovies 
where Movies.MovieID=RentedMovies.MovieIDFK group by MovieId,Title order by ordertimes desc