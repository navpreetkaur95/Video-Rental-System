select FirstName,LastName,count(rmid) as ordertimes from customer, rentedmovies 
where Customer.CustID=RentedMovies.CustIDFK group by FirstName,LastName order by ordertimes desc