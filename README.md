       int pageSize = 5;
       var queryCoordinators = context.AppCoordinatorMasters.AsQueryable();

       if (!string.IsNullOrEmpty(searchString))
           queryCoordinators = queryCoordinators.Where(c => c.Pno.Contains(searchString));

       var data = queryCoordinators.OrderBy(c => c.Pno).ToList();
       var pagedData = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

       ViewBag.pList = pagedData;
       ViewBag.CurrentPage = page;
       ViewBag.TotalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
       ViewBag.SearchString = searchString;
