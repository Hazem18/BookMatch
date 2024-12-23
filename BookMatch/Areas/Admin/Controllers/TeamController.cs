﻿using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.ViewModels;
using System.Linq.Expressions;
using Utility;

namespace BookMatch.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.AdminRole)]

    public class TeamController : Controller
    {
        private readonly ITeamRepository teamRepository;
        private readonly IStadiumRepository stadiumRepository;
        private readonly ILeagueRepository leagueRepository;

        public TeamController(ITeamRepository teamRepository, IStadiumRepository stadiumRepository, ILeagueRepository leagueRepository)
        {
            this.teamRepository=teamRepository;
            this.stadiumRepository=stadiumRepository;
            this.leagueRepository=leagueRepository;
        }

        public IActionResult Index()
        {
            var teams = teamRepository.GetTeams(
                    includeStadium: true,  
                    includeLeagues: true    
                                                );
            return View(teams);
        }
        public IActionResult Create()
        {
            
            LoadBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TeamVM teamVM , IFormFile LogoUrl)
        {
            if (ModelState.IsValid)
            {
                var team = new Team();
                team.Name = teamVM.Name;
                team.StadiumId = teamVM.StadiumId;
                var fileName = UploadImg(LogoUrl);
                team.LogoUrl = fileName;

                if (teamVM.SelectedLeagueIds != null && teamVM.SelectedLeagueIds.Any())
                {
                    foreach (var leagueId in teamVM.SelectedLeagueIds)
                    {
                        team.TeamLeagues.Add(new TeamLeague
                        {
                            LeagueId = leagueId
                        });
                    }
                }
                teamRepository.Create(team);
                teamRepository.Commit();
                return RedirectToAction("Index");
            }
            LoadBags();
            return View(teamVM);
        }

        public IActionResult Edit(int Id)
        {
            var team = teamRepository
                .GetTeams(includeLeagues: true, includeStadium: true)
                .FirstOrDefault(x => x.Id == Id);

            if (team == null)
            {
                return RedirectToAction("Notfound", "Home", new { area = "User" });
            }

            var teamVM = new TeamVM
            {
                Id = team.Id,
                Name = team.Name,
                StadiumId = team.StadiumId,
                SelectedLeagueIds = team.TeamLeagues.Select(tl => tl.LeagueId).ToList(),
                LogoUrl = team.LogoUrl 
            };

            LoadBags();
            return View(teamVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, IFormFile LogoUrl, TeamVM teamVM)
        {
            ModelState.Remove("LogoUrl");
            if (ModelState.IsValid)
            {
                var team = teamRepository.Get(
                    includeProps: new Expression<Func<Team, object>>[] { t => t.TeamLeagues },
                    expression: t => t.Id == Id
                ).FirstOrDefault();

                if (team == null)
                {
                    return RedirectToAction("Notfound", "Home", new { area = "User" });
                }

                team.Name = teamVM.Name;
                team.StadiumId = teamVM.StadiumId;

                if (LogoUrl != null && LogoUrl.Length > 0)
                {
                    team.LogoUrl = UploadImg(LogoUrl) ?? teamVM.LogoUrl;
                }

                var existingLeagueIds = team.TeamLeagues.Select(tl => tl.LeagueId).ToList();
                var leaguesToAdd = teamVM.SelectedLeagueIds.Except(existingLeagueIds).ToList();
                var leaguesToRemove = existingLeagueIds.Except(teamVM.SelectedLeagueIds).ToList();

                foreach (var leagueId in leaguesToAdd)
                {
                    team.TeamLeagues.Add(new TeamLeague { LeagueId = leagueId });
                }

                foreach (var leagueId in leaguesToRemove)
                {
                    var teamLeague = team.TeamLeagues.FirstOrDefault(tl => tl.LeagueId == leagueId);
                    if (teamLeague != null)
                    {
                        team.TeamLeagues.Remove(teamLeague);
                    }
                }

                teamRepository.Edit(team);
                teamRepository.Commit();

                return RedirectToAction("Index");
            }

            LoadBags();
            return View(teamVM);
        }

        public IActionResult Delete(int Id)
        {
            var team = teamRepository.Get(
                includeProps: new Expression<Func<Team, object>>[] { t => t.TeamLeagues },
                expression: t => t.Id == Id
                ).FirstOrDefault();

            if (team == null)
            {
                return RedirectToAction("Notfound", "Home", new { area = "User" });
            }

            
            team.TeamLeagues.Clear();

            teamRepository.Delete(team);
            teamRepository.Commit();

            return RedirectToAction("Index");
        }

        private void LoadBags()
        {
            ViewBag.leagues = leagueRepository.Get();
            ViewBag.stadiums = stadiumRepository.Get();
        }

        
        private string? UploadImg(IFormFile ImgUrl)
        {
            if (ImgUrl.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Teams", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    ImgUrl.CopyTo(stream);
                }
                return fileName;
            }
            return null;
        }
        private string UpdateImage(IFormFile ImgUrl, int TeamId)
        {
            if (ImgUrl != null && ImgUrl.Length > 0)
            {
                return UploadImg(ImgUrl) ?? string.Empty;
            }
            else
            {

                var existingTeam = teamRepository.GetOne(expression: e => e.Id == TeamId);
                return existingTeam.LogoUrl;
            }
        }

    }
}
